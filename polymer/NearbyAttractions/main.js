function updateState(t){
  t.isHome= t.state == "home";
  t.isDay= t.state == "day";
  t.isHotel= t.state == "hotel";
  t.isFeature= t.state == "featureFromHotel" ||t.state == "featureFromDay";
  t.isFeatureWasHotel= t.state == "featureFromHotel";
  t.isFeatureWasDay= t.state == "featureFromDay";
  t.isNearby= t.state == "nearbyFromHotel" ||t.state == "nearbyFromDay";
  t.isNearbyWasHotel= t.state == "nearbyFromHotel";
  t.isNearbyWasDay= t.state == "nearbyFromDay";
  
  if(t.isHome){
    t.currentZoom = t.options.homeZoom;
    t.currentLatitude = t.homebase.latitude;
    t.currentLongitude = t.homebase.longitude;
  }
  else if(t.isDay){
    t.currentZoom = t.options.dayZoom;
    t.currentLatitude = t.day.dayLatitude;
    t.currentLongitude = t.day.dayLongitude;
  }
  else if(t.isHotel){
    t.currentZoom = t.options.hotelZoom;
    t.currentLatitude = t.day.hotelLatitude;
    t.currentLongitude = t.day.hotelLongitude;
  }
  else if(t.isFeature){
    t.currentZoom = t.options.featureZoom;
    t.currentLatitude = t.feature.latitude;
    t.currentLongitude = t.feature.longitude;
  }
  else if(t.isNearby){
    t.currentZoom = t.options.featureZoom;
    t.currentLatitude = t.nearby.latitude;
    t.currentLongitude = t.nearby.longitude;
  }
}

      /*
  $.ajax({
    url: "http://usb-tgcollabap2:8077/CollabatronMaps2015/api/TripInfo/"+prodId,
    type: "GET",
    crossDomain: true,
    success: function(data) {
      
      console.log(t.days);
      
      Polymer.updateStyles();
      //document.querySelector("#dayRepeat").refresh();
    },
    error: function(data) {
      console.log("error");
      console.log(data);
    }
  });
    */

