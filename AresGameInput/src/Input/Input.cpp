#include <iostream>
#include <stdio.h>
#include "Input.h"

char Input::setAndGetKey() {
  system("stty raw");
  this->key = getchar();
  system("stty cooked");

  return this->key;
}