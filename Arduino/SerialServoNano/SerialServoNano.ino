#include <SoftwareSerial.h>
#include <SerialCommand.h>
SerialCommand sCmd;

#include <Servo.h>

// Variable declarations
Servo myServo1, myServo2;
int servo1Pin = 3; // Uno: PWM pin 10
int servo2Pin = 5; // Nano: PWM 3 and 5
int servoAngle = 90;

// HS-125MG Wiriing: Black-Ground, Red-Power, Yellow-Signal

void setup() {
  Serial.begin(9600);        // set up Serial library at 9600 bps
  myServo1.attach(servo1Pin);
  myServo2.attach(servo2Pin);
  
  while (!Serial);
  
  //sCmd.addCommand("PING", pingHandler);
}

void loop() {
  //Serial.print("Hi");
  if (Serial.available() > 0){
    //sCmd.readSear
    //Serial.println(Serial.read());
    servoAngle = Serial.parseInt();
    
    if (servoAngle == 300){
      myServo1.detach();
      myServo2.detach();
      Serial.println("Detaching servo");
    }
    else if (servoAngle != 0){
      myServo1.attach(servo1Pin);
      myServo2.attach(servo2Pin);
      myServo1.write(servoAngle);
      myServo2.write(servoAngle);
      Serial.print("Servo angle set at ");
      Serial.print(myServo1.read(), DEC);
      Serial.print(" Read: ");
      Serial.println(servoAngle);
    }
    else{
      // Heartbeat message
      Serial.println(servoAngle);
    }
    
    Serial.flush();
  }
}
