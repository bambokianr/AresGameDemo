#include <iostream>
#include <X11/Xlib.h>
#include "XWindow.h"

using namespace std;

#define WINDOW_POSX         0
#define WINDOW_POSY         0
#define WINDOW_WIDTH      750
#define WINDOW_HEIGHT     750
#define WINDOW_BORDER       0

#define KEY_W              21 
#define KEY_A               8
#define KEY_S               9 
#define KEY_D              10 
#define KEY_ARROWUP       134
#define KEY_ARROWLEFT     131 
#define KEY_ARROWDOWN     133
#define KEY_ARROWRIGHT    132
#define KEY_SPACE          57
#define KEY_ESC            61

Window XWindow::createWindow(Window root, int screen) {
  Window win;
  XSetWindowAttributes xwa;

  xwa.background_pixel = WhitePixel(dpy, screen);
  xwa.border_pixel = BlackPixel(dpy, screen);
  xwa.event_mask = KeyPressMask | KeyReleaseMask | ButtonPressMask | ButtonReleaseMask | PointerMotionMask;

  win = XCreateWindow(dpy, root, WINDOW_POSX, WINDOW_POSY, WINDOW_WIDTH, WINDOW_HEIGHT, WINDOW_BORDER,
                      DefaultDepth(dpy, screen), InputOutput, DefaultVisual(dpy, screen),
                      CWBackPixel | CWBorderPixel | CWEventMask, &xwa);

  return win;
}

XWindow::XWindow() {
  if((dpy = XOpenDisplay(NULL)) == NULL) {
    cout << endl << "can't open display" << endl;
    exit(-1);
  }

  int screen = DefaultScreen(dpy);
  Window root = RootWindow(dpy, screen);
  
  win = createWindow(root, screen);
  XMapWindow(dpy, win);
}

void XWindow::positionCursorInCenter() {
  XWarpPointer(dpy, None, win, 0, 0, 0, 0, (int)(WINDOW_WIDTH / 2), (int)(WINDOW_HEIGHT / 2));
}

void XWindow::handleKeyCodeInputValue(int inputs[], int keycode, int value) {
  if(keycode == KEY_W || keycode == KEY_ARROWUP)
    inputs[0] = value;
  if(keycode == KEY_A || keycode == KEY_ARROWLEFT)
    inputs[1] = value;
  if(keycode == KEY_S || keycode == KEY_ARROWDOWN)
    inputs[2] = value;
  if(keycode == KEY_D || keycode == KEY_ARROWRIGHT)
    inputs[3] = value;
  if(keycode == KEY_SPACE)
    inputs[4] = value;
}

void XWindow::handleEvent(int inputs[]) {
  while(XNextEvent(dpy, &ev) == 0) {
    cout << "{ " << inputs[0] << ", " << inputs[1] << ", " << inputs[2] << ", " << inputs[3] << ", " << inputs[4] << ", " << inputs[5] << ", " << inputs[6] << ", " << inputs[7] << " }" << endl;
    switch(ev.type) {
      case KeyPress:
        XWindow::handleKeyCodeInputValue(inputs, ev.xkey.keycode, 1);
        break;
      case KeyRelease:
        XWindow::handleKeyCodeInputValue(inputs, ev.xkey.keycode, 0);
        break;
      case ButtonPress:
        inputs[5] = 1;
        break;
      case ButtonRelease:
        inputs[5] = 0;
        break;
      case MotionNotify:
        if(ev.xmotion.x > WINDOW_WIDTH || ev.xmotion.x < 0 
        || ev.xmotion.y > WINDOW_HEIGHT || ev.xmotion.y < 0)
          positionCursorInCenter();
        inputs[6] = ev.xmotion.x;
        inputs[7] = ev.xmotion.y;
        break;
      default:
        break;
      }
  }
}

void XWindow::cleanUp() {
  XUnmapWindow(dpy, win);
  XDestroyWindow(dpy, win);
  XCloseDisplay(dpy);
}

int main() {
  // [0] = keys W | UP
  // [1] = keys A | LEFT
  // [2] = keys S | DOWN
  // [3] = keys D | RIGHT
  // [4] = key SPACE
  // [5] = mouse click
  // [6] = mouse deltaX
  // [7] = mouse deltaY
  int userInputs[] = { 0, 0, 0, 0, 0, 0, 0, 0 };
  XWindow xWindow;
   
  xWindow.handleEvent(userInputs);
  xWindow.cleanUp();
  
  return 0;
}
