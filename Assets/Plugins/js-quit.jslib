var CloseWindowPlugin = {
  closeWindow: function () {
    window.close();
  },
};
mergeInto(LibraryManager.library, CloseWindowPlugin);
