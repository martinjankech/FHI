import {Component, OnInit, } from '@angular/core';
import {environment} from '../../environments/environment';

@Component({
  selector: 'app-map-location',
  templateUrl: './map-location.component.html',
  styles: [
  ]
})
export class MapLocationComponent implements OnInit {
  Latitude:number
  Longitude:number

  constructor() { }

  ngOnInit(): void {
  }

  findMe() {

      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition((position) => {
          this.Latitude =  position.coords.latitude;
          this.Longitude=  position.coords.longitude;

        });
      } else {
        alert("Geolocation is not supported by this browser.");
      }
    }










}
