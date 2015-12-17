var toastGroupTemplate = document.querySelector('#toastGroup');
toastGroupTemplate.showToast = function() {
  document.querySelector('#toast').show();
};
Polymer({
  is:'dom-bind',
  properties: {
    features: [
      {category:'pizza',name:'Pepe\'s Pizzaria Neopoliana'},
      {category:'pizza',name:'Salley\'s Apizza'},
      {category:'other',name:'check this out'}]
  }
});