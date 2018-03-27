#include <SoftwareSerial.h>
#include <SerialCommand.h>
SerialCommand sCmd;

#include <Servo.h>

// Variable declarations
Servo myServo;
int servoPin = 10; // Change according to board
int servoAngle = 90;

void setup() {
  Serial.begin(9600);        // set up Serial library at 9600 bps
  myServo.attach(servoPin);
  
  while (!Serial);
  
  //sCmd.addCommand("PING", pingHandler);

  
}

void loop() {
  if (Serial.available() > 0){
    //sCmd.readSear
    servoAngle = Serial.parseInt();

    if (servoAngle == 300){
      myServo.detach();
    }
    else{
      myServo.attach(servoPin);
      myServo.write(servoAngle);
      Serial.print("Servo angle set at ");
      Serial.print(myServo.read(), DEC);
      Serial.print(" Read: ");
      Serial.println(servoAngle);
    }
    
  }
}
