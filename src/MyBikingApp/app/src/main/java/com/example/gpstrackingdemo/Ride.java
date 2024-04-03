package com.example.gpstrackingdemo;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class Ride {
    private List<Point> points = new ArrayList<>();
    private List<WheeleRide> wheeleRides = new ArrayList<WheeleRide>();
    private String startingDateTime;
    private String endingDateTime;
//    private String bikeId;
    private double distance;
    public void AddPoint(Point point){
        points.add(point);
    }
    public void AddWheeleRide(WheeleRide wheeleRide) {
        wheeleRides.add(wheeleRide);
    }
    public String getStartingDateTime() {
        return startingDateTime;
    }

    public void setStartingDateTime(String startingDateTime) {
        this.startingDateTime = startingDateTime;
    }

    public String getEndingDateTime() {
        return endingDateTime;
    }

    public void setEndingDateTime(String endingDateTime) {
        this.endingDateTime = endingDateTime;
    }

//    public String getBikeId() {
//        return bikeId;
//    }
//
//    public void setBikeId(String bikeId) {
//        this.bikeId = bikeId;
//    }

    public void setDistance(double distance) {
        this.distance = distance;
    }
}
