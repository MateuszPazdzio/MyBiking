package com.example.gpstrackingdemo;

public class WheeleItem {
    private double speed;
    private String address;
    private double distance;
    private double maxRotateX;
    private Point point;
    private double altitude;

    public WheeleItem(Point point, float speed, double altitude, String address, double calcDistance,Float  maxRotateX) {

        this.point = point;
        this.speed = speed;
        this.altitude = altitude;
        this.address = address;
        this.distance = calcDistance;
        this.maxRotateX = maxRotateX;
    }
}
