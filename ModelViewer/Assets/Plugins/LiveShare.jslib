mergeInto(LibraryManager.library, {

  UpdateCurrentRotation: function (x,y,z) {
    window.parent.updateCurrentRotation(x,y,z);
  },

  JSConsoleLog: function (str) {
    console.log(UTF8ToString(str));
  },

});