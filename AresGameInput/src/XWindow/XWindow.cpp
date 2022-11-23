#include <stdio.h>
#include <err.h>
#include <X11/X.h>
#include <X11/Xlib.h>
#include <X11/Xutil.h>
#include <X11/XKBlib.h>
#include "XWindow.h"

Window XWindow::createWindow(int x, int y, int w, int h, int b) {
  Window win;
  XSetWindowAttributes xwa;

  xwa.background_pixel = WhitePixel(dpy, scr);
  xwa.border_pixel = BlackPixel(dpy, scr);
  xwa.event_mask = KeyPressMask | KeyReleaseMask | ButtonPressMask | ButtonReleaseMask | PointerMotionMask;

  win = XCreateWindow(dpy, root, x, y, w, h, b,
                      DefaultDepth(dpy, scr), InputOutput, DefaultVisual(dpy, scr),
                      CWBackPixel | CWBorderPixel | CWEventMask, &xwa);

  return win;
}

XWindow::XWindow(int xPos, int yPos, int width, int height, int borderWidth) {
  if((dpy = XOpenDisplay(NULL)) == NULL)
    errx(1, "Can't open display.");

  scr = DefaultScreen(dpy);
  root = RootWindow(dpy, scr);
  
  win = createWindow(xPos, yPos, width, height, borderWidth);
  XMapWindow(dpy, win);
}

void XWindow::handleEvent() {
  while(XNextEvent(dpy, &ev) == 0) {
    switch(ev.type) {
    case KeyPress:
      printf("key pressed\n");
      break;
    case KeyRelease:
      printf("key released\n");
      break;
    case ButtonPress:
      printf("button pressed\n");
      break;
    case ButtonRelease:
      printf("button released\n");
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


#define POSX      0
#define POSY      0
#define WIDTH   500
#define HEIGHT  500
#define BORDER   15

int main() {
  XWindow xWindow(POSX, POSY, WIDTH, HEIGHT, BORDER);
  xWindow.handleEvent();
  xWindow.cleanUp();
  return 0;
}