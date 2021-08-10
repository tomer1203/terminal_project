/*
 * file_system.c
 *
 *  Created on: Aug 10, 2021
 *      Author: tomer
 */
void initialize_file_system(){
	int i=0;
	file_system.start_address = FILE_SYSTEM_START_ADDRESS;
	file_system.end_address   = FILE_SYSTEM_END_ADDRESS;
	
	file_system.number_of_files   = 0;
	file_system.state             = IDLE_FS;

	file_system.write_pointer     = file_system.start_address;
	file_system.size_remaining    = 0;
	file_system.first_file		  = 0;
	file_system.last_file		  = 0;
	
	file_system.current_read_file = 0;
	file_system.read_pointer      = file_system.start_address;
	
	// Initialise the files
	for (i = 0; i < MAX_NUMBER_OF_FILES;i++){
		initialize_file_desc(&file_system.file_list[i]);
	}
}
initialize_file_desc(File_descriptor * file_desc){
	int i = 0;
	for (int i=0;i<MAX_NAME_SIZE;i++){
		file_desc->name[i] = 0;
	}
	file_desc->size          = 0;
	file_desc->start_pointer = NULL;
	file_desc->valid         = 0;
}

// read_file_init_message //
// change state to READ_FILE_FS
// set read_file and read_pointer
// return success or fail

// read_line         //
// check if we are in correct state
// check if there are more lines to read
// read one line(16 Bytes) update read pointer
// return either the line or null for failure


// write_file_init_message //
int write_file_init_message(char* message){
	if (file_system.state == WRITE_SIZE_FS){
		// writing size
		if (!is_size_command(message)){
			return -1;
		}
		
		
	} else {
		// writing name
		if (!is_name_command(message)){
			return -1;
		}
		initialize_file_desc(&file_system.temp_file_desc);
		strcpy(&file_system.temp_file_desc.name,message);
		file_system.temp_file_desc.start_pointer = 
				file_system.file_list[file_system.last_file].start_pointer + 
				file_system.file_list[file_system.last_file].size;
		file_system.temp_file_desc.valid = 0;
		return 0;
	}
}
// check what message has been received and act accordingly
// check if there is space and if not remove old files
// set basic stats on the write operation
// returns if initialization is complete

// assumed message order:
// size
// name

// write_file_chunck //
// 
// check if we are in correct state
// check if size of chunk fits the size of the message
// send chunk to dma for sending to memory
