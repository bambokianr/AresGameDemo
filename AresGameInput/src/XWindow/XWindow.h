#include <X11/Xlib.h>

class XWindow {
  private:
    Display *dpy;
    Window win;
    XEvent ev;
    Window createWindow(Window root, int screen);
    void positionCursorInCenter();
    void handleKeyCodeInputValue(int inputs[], int keycode, int value);

    public: 
      XWindow();
      void handleEvent(int inputs[]);
      void cleanUp();
};