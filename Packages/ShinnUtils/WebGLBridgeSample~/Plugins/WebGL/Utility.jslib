var UtilityPlugin = {
  unityMessage: function (event) {
    window.dispatchReactUnityEvent("unityMessage", UTF8ToString(event));
  },
};

mergeInto(LibraryManager.library, UtilityPlugin);
