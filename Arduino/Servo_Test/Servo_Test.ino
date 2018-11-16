#include <SoftwareSerial.h>
#include <SerialCommand.h>
SerialCommand sCmd;

#include <Servo.h>

// Variable declarations
Servo myServo;
int servoPin = 10; // Change according to board
int servoAngle = 90;

// HS-125MG Wiriing: Black-Ground, Red-Power, Yellow-Signal

void setup() {
  Serial.begin(9600);        // set up Serial library at 9600 bps
  myServo.attach(servoPin);
  
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
      myServo.detach();
      Serial.println("Detaching servo");
    }
    else if (servoAngle != 0){
      myServo.attach(servoPin);
      myServo.write(servoAngle);
      Serial.print("Servo angle set at ");
      Serial.print(myServo.read(), DEC);
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
