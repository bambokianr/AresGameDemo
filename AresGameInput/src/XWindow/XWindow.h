#include <X11/Xlib.h>

class XWindow {
  private:
    Display *dpy;
    int scr;
    Window root;
    XEvent ev;

    Window win;
    Window createWindow(int x, int y, int w, int h, int b);

    public:
      XWindow(int xPos, int yPos, int width, int height, int borderWidth);
      void handleEvent();
      void cleanUp();
};