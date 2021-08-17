/*
 * file_system.c

 *
 *  Created on: Aug 10, 2021
 *      Author: tomer
 */
#include "TFC.h"

int index_cyclic_plusplus(int old_index);
int index_cyclic_minusminus(int old_index);
int address_cyclic_add(int address,int added_value);

char     reading_Line[LINE_LENGTH];
uint8_t  fs_memory[SIZE_OF_FILE_SYSTEM];

void initialize_file_system(){
	int i=0;
	file_system.start_address = FILE_SYSTEM_START_ADDRESS;
	file_system.end_address   = FILE_SYSTEM_END_ADDRESS;
	
	file_system.number_of_files       = 0;
	file_system.state                 = IDLE_FS;
	file_system.system_size_remaining = SIZE_OF_FILE_SYSTEM; 
	file_system.write_pointer         = file_system.start_address;
	file_system.size_remaining        = 0;
	file_system.first_file		      = 0;
	file_system.last_file		      = 0;
	
	file_system.read_file     = 0;
	file_system.read_pointer          = file_system.start_address;
	
	// Initialize the files
	for (i = 0; i < MAX_NUMBER_OF_FILES;i++){
		initialize_file_desc(&file_system.file_list[i]);
	}
}
void initialize_file_desc(File_descriptor *file_desc){
	int i = 0;
	for (i=0;i<MAX_NAME_SIZE;i++){
		file_desc->name[i] = 0;
	}
	file_desc->size          = 0;
	file_desc->start_pointer = NULL;
	file_desc->valid         = 0;
}

// file_info //
File_descriptor* file_info(int index){
	return &file_system.file_list[index];
}

// read_file_init //
// change state to READ_FILE_FS
// set read_file and read_pointer
// return success or fail
int read_file_init(int file_num){
	file_system.state = READ_FILE_FS;
	file_system.read_file = file_num;
	file_system.read_pointer = file_system.file_list[file_num].start_pointer;

	return 0;
}


// read_line         //
// check if we are in correct state
// check if there are more lines to read
// read one line(16 Bytes) update read pointer
// return either the line or null for failure
int read_line(){
	int read_amount = 16;
	char* end_line_address;
	char* end_of_file_address;
	char* read_line_start;
	if (file_system.state != READ_FILE_FS){
		return NULL;
	}

	// decide how much you can read
	end_line_address    = (char*)address_cyclic_add((int)file_system.read_pointer,16);
	end_of_file_address = (char*)address_cyclic_add((int)file_system.file_list[file_system.read_file].start_pointer,file_system.file_list[file_system.read_file].size);
	read_line_start = file_system.read_pointer;
	if (end_line_address > end_of_file_address){
		read_amount = end_of_file_address - file_system.read_pointer;
		file_system.read_pointer = 0;
		
	}
	else {
		file_system.read_pointer += 16;
	}

	//** TODO READ 16 BYTES FROM DMA TODO **//
	//reading_Line = value here;
	for (i = 0;i < read_amount;i++) {
		reading_Line = file_system.read_pointer[i];
	}
	reading_Line[i] = '\0'

	return read_amount;
}

// write_file_init_message //
// check what message has been received and act accordingly
// check if there is space and if not remove old files
// set basic stats on the write operation
// returns if initialization is complete
// assumed message order:
// name
// size
int write_file_init_message(char* message){
	char * size_str;
	int size;
	int size_temp;
	int index_temp;
	
	// set up the write pointer
	file_system.write_pointer =  
			(char*)address_cyclic_add((int)(file_system.file_list[file_system.last_file].start_pointer),
					file_system.file_list[file_system.last_file].size);
	
	if (file_system.state == WRITE_SIZE_FS){
		// writing size
		if (!is_size_command(message)){
			return -1;
		}
		// this is a valid size command
		size_str = strip_command(message);
		size = atoi(size_str);
		
		// check that there is enough space for the new file
		if (size > SIZE_OF_FILE_SYSTEM){
			return -2;
		}
		
		// clear files to make space for the new file
		while (file_system.system_size_remaining < size){
			if (file_system.number_of_files == 0){
				// this is tested earlier so should never happen
				return -12;
			}
			file_system.file_list[file_system.first_file].valid = 0;
			file_system.system_size_remaining  += file_system.file_list[file_system.first_file].size;
			file_system.number_of_files -= 1;
			file_system.first_file      = index_cyclic_plusplus(file_system.first_file);
		}
		
		// add file to system
		file_system.system_size_remaining -= size;
		file_system.temp_file_desc.size = size;
		file_system.size_remaining = size;
		// change address of last file to point to new file location
		file_system.last_file = index_cyclic_plusplus(file_system.last_file); 
		file_system.file_list[file_system.last_file] = file_system.temp_file_desc;
		file_system.state = WRITE_DATA_FS;
		return 1;
		
	} else {
		// writing name
		if (!is_name_command(message)){
			return -3;
		}
		initialize_file_desc(&file_system.temp_file_desc);
		strcpy(&file_system.temp_file_desc.name,strip_command(message));
		file_system.temp_file_desc.start_pointer = 
				file_system.file_list[file_system.last_file].start_pointer + 
				file_system.file_list[file_system.last_file].size;
		file_system.temp_file_desc.valid = 0;
		file_system.state = WRITE_SIZE_FS;
		return 0;
	}
}


// write_file_chunck //
// copies the write_data into the memory
// this should be called after reading a chunck from the memory
// check if we are in correct state
// check if size of chunk fits the size of the message
// send chunk to dma for sending to memory
// check if we finished the message and if so turn the valid into true.
int write_file_chunck(char* write_data, int size){
	int i = 0;
	if (file_system.state != WRITE_DATA_FS){
		return -1; // entered in wrong state
	}
	if (size > file_system.size_remaining){
		return -2; // space allocated to file is done
	}

	file_system.size_remaining -= size;
	//** TODO SEND TO DMA TODO **//
	for (i = 0;i < size;i++) {
		file_system.write_pointer[i] = write_data[i];
	}
	
	// finished reading the file
	if (file_system.size_remaining == 0){
		file_system.state = IDLE_FS;
		file_system.file_list[file_system.last_file].valid = 1;
		return 1;
	}
	return 0;
}

int remove_last_file(){
	file_system.last_file = index_cyclic_minusminus(file_system.last_file);
	return 0;
}

int address_cyclic_add(int address,int added_value){
	return ((int)(FILE_SYSTEM_START_ADDRESS))+(address-((int)(FILE_SYSTEM_START_ADDRESS))+added_value)%SIZE_OF_FILE_SYSTEM;
}

int file_index_plusplus_with_menu(int file_index) {
	if (file_index == file_system.last_file+1) {
		return file_system.first_file+1;
	}
	return index_cyclic_plusplus(file_index);
}

int file_index_plusplus(int file_index){
	if (file_index == file_system.last_file){
		return file_system.first_file;
	}
	return index_cyclic_plusplus(file_index);
}
int file_index_minusminus(int file_index){
	if (file_index == file_system.first_file){
		return file_system.last_file;
	}
	return index_cyclic_minusminus(file_index);
}

// ++ action for indexes
int index_cyclic_plusplus(int old_index){
	if (old_index >= MAX_NUMBER_OF_FILES-1){
		return 0;
	}
	return old_index+1;
}
// -- action for indexes
int index_cyclic_minusminus(int old_index){
	if (old_index <= 0){
		return MAX_NUMBER_OF_FILES-1;
	}
	return old_index-1;
}
