#include <Servo.h>

// Variable declarations
Servo myServo;
int servoPin = 10; // Change according to board
int servoAngle = 0;

void setup() {
  Serial.begin(9600);        // set up Serial library at 9600 bps

  myServo.attach(servoPin);
}

void loop() {
  if (Serial.available() > 0){
    servoAngle = Serial.read();
    myServo.write(servoAngle);
    Serial.println("Servo angle set at " + myServo.read());
  }
  
}
