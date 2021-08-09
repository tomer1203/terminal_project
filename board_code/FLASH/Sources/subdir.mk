################################################################################
# Automatically-generated file. Do not edit!
################################################################################

-include ../makefile.local

# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS_QUOTED += \
"../Sources/ADCandDAC.c" \
"../Sources/BoardSupport.c" \
"../Sources/Fsm.c" \
"../Sources/UART.c" \
"../Sources/arm_cm0.c" \
"../Sources/convertToAscii.c" \
"../Sources/halGPIO.c" \
"../Sources/halLCD.c" \
"../Sources/main.c" \
"../Sources/mcg.c" \
"../Sources/sa_mtb.c" \
"../Sources/user_interface.c" \

C_SRCS += \
../Sources/ADCandDAC.c \
../Sources/BoardSupport.c \
../Sources/Fsm.c \
../Sources/UART.c \
../Sources/arm_cm0.c \
../Sources/convertToAscii.c \
../Sources/halGPIO.c \
../Sources/halLCD.c \
../Sources/main.c \
../Sources/mcg.c \
../Sources/sa_mtb.c \
../Sources/user_interface.c \

OBJS += \
./Sources/ADCandDAC.o \
./Sources/BoardSupport.o \
./Sources/Fsm.o \
./Sources/UART.o \
./Sources/arm_cm0.o \
./Sources/convertToAscii.o \
./Sources/halGPIO.o \
./Sources/halLCD.o \
./Sources/main.o \
./Sources/mcg.o \
./Sources/sa_mtb.o \
./Sources/user_interface.o \

C_DEPS += \
./Sources/ADCandDAC.d \
./Sources/BoardSupport.d \
./Sources/Fsm.d \
./Sources/UART.d \
./Sources/arm_cm0.d \
./Sources/convertToAscii.d \
./Sources/halGPIO.d \
./Sources/halLCD.d \
./Sources/main.d \
./Sources/mcg.d \
./Sources/sa_mtb.d \
./Sources/user_interface.d \

OBJS_QUOTED += \
"./Sources/ADCandDAC.o" \
"./Sources/BoardSupport.o" \
"./Sources/Fsm.o" \
"./Sources/UART.o" \
"./Sources/arm_cm0.o" \
"./Sources/convertToAscii.o" \
"./Sources/halGPIO.o" \
"./Sources/halLCD.o" \
"./Sources/main.o" \
"./Sources/mcg.o" \
"./Sources/sa_mtb.o" \
"./Sources/user_interface.o" \

C_DEPS_QUOTED += \
"./Sources/ADCandDAC.d" \
"./Sources/BoardSupport.d" \
"./Sources/Fsm.d" \
"./Sources/UART.d" \
"./Sources/arm_cm0.d" \
"./Sources/convertToAscii.d" \
"./Sources/halGPIO.d" \
"./Sources/halLCD.d" \
"./Sources/main.d" \
"./Sources/mcg.d" \
"./Sources/sa_mtb.d" \
"./Sources/user_interface.d" \

OBJS_OS_FORMAT += \
./Sources/ADCandDAC.o \
./Sources/BoardSupport.o \
./Sources/Fsm.o \
./Sources/UART.o \
./Sources/arm_cm0.o \
./Sources/convertToAscii.o \
./Sources/halGPIO.o \
./Sources/halLCD.o \
./Sources/main.o \
./Sources/mcg.o \
./Sources/sa_mtb.o \
./Sources/user_interface.o \


# Each subdirectory must supply rules for building sources it contributes
Sources/ADCandDAC.o: ../Sources/ADCandDAC.c
	@echo 'Building file: $<'
	@echo 'Executing target #1 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/ADCandDAC.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/ADCandDAC.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/BoardSupport.o: ../Sources/BoardSupport.c
	@echo 'Building file: $<'
	@echo 'Executing target #2 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/BoardSupport.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/BoardSupport.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/Fsm.o: ../Sources/Fsm.c
	@echo 'Building file: $<'
	@echo 'Executing target #3 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/Fsm.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/Fsm.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/UART.o: ../Sources/UART.c
	@echo 'Building file: $<'
	@echo 'Executing target #4 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/UART.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/UART.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/arm_cm0.o: ../Sources/arm_cm0.c
	@echo 'Building file: $<'
	@echo 'Executing target #5 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/arm_cm0.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/arm_cm0.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/convertToAscii.o: ../Sources/convertToAscii.c
	@echo 'Building file: $<'
	@echo 'Executing target #6 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/convertToAscii.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/convertToAscii.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/halGPIO.o: ../Sources/halGPIO.c
	@echo 'Building file: $<'
	@echo 'Executing target #7 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/halGPIO.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/halGPIO.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/halLCD.o: ../Sources/halLCD.c
	@echo 'Building file: $<'
	@echo 'Executing target #8 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/halLCD.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/halLCD.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/main.o: ../Sources/main.c
	@echo 'Building file: $<'
	@echo 'Executing target #9 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/main.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/main.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/mcg.o: ../Sources/mcg.c
	@echo 'Building file: $<'
	@echo 'Executing target #10 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/mcg.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/mcg.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/sa_mtb.o: ../Sources/sa_mtb.c
	@echo 'Building file: $<'
	@echo 'Executing target #11 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/sa_mtb.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/sa_mtb.o"
	@echo 'Finished building: $<'
	@echo ' '

Sources/user_interface.o: ../Sources/user_interface.c
	@echo 'Building file: $<'
	@echo 'Executing target #12 $<'
	@echo 'Invoking: ARM Ltd Windows GCC C Compiler'
	"$(ARMSourceryDirEnv)/arm-none-eabi-gcc" "$<" @"Sources/user_interface.args" -MMD -MP -MF"$(@:%.o=%.d)" -o"Sources/user_interface.o"
	@echo 'Finished building: $<'
	@echo ' '


