// File: MyPlugin.jslib

mergeInto(LibraryManager.library, {


  GameOver: function (userName, score) {
    window.dispatchReactUnityEvent(
      "GameOver",
      Pointer_stringify(userName),
      score
    );
  },

  UnitySend: function (str) {
    window.alert(Pointer_stringify(str));
  },

  UnitySendArgs: function (str) {
     window.dispatchReactUnityEvent("UnitySendArgs", Pointer_stringify(str));
  },

});