package com.example.gpstrackingdemo;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class Ride {
    private List<Point> pointList = new ArrayList<>();
    private List<WheeleRide> wheeleRideList = new ArrayList<WheeleRide>();
    private LocalDateTime startingDateTime;
    private LocalDateTime endingDateTime;
    private String bikeId;
    private double distance;
    public void AddPoint(Point point){
        pointList.add(point);
    }
    public void AddWheeleRide(WheeleRide wheeleRide) {
        wheeleRideList.add(wheeleRide);
    }
    public LocalDateTime getStartingDateTime() {
        return startingDateTime;
    }

    public void setStartingDateTime(LocalDateTime startingDateTime) {
        this.startingDateTime = startingDateTime;
    }

    public LocalDateTime getEndingDateTime() {
        return endingDateTime;
    }

    public void setEndingDateTime(LocalDateTime endingDateTime) {
        this.endingDateTime = endingDateTime;
    }

    public String getBikeId() {
        return bikeId;
    }

    public void setBikeId(String bikeId) {
        this.bikeId = bikeId;
    }

    public void setDistance(double distance) {
        this.distance = distance;
    }
}
