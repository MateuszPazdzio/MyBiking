package com.example.gpstrackingdemo;

import android.location.Geocoder;

public class Point {
    private double latitude;
    private double longitude;
    private String address;

    public Point(double latitude, double longitude, String address){
        this.latitude = latitude;
        this.longitude = longitude;
        this.address = address;
    }

    public double getLatitude() {
        return latitude;
    }

    public double getLongitude() {
        return longitude;
    }
}
