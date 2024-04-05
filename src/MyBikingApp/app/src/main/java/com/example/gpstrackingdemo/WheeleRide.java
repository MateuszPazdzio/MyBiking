package com.example.gpstrackingdemo;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class WheeleRide {
    private List<WheeleItem> wheeleItems = new ArrayList<>();
    private String durationTime;
    private String startingDateTime;
    private String endingDateTime;
    private double distance;

    public WheeleRide(String startingDateTime) {
        this.startingDateTime = startingDateTime;
    }

    public void AddIWheeleItem(WheeleItem wheeleItem){
        wheeleItems.add(wheeleItem);
    }

    public List<WheeleItem> getWheeleItemList() {
        return this.wheeleItems;
    }
    public void setWheeleItemList(List<WheeleItem> wheeleItemList) {
        this.wheeleItems = wheeleItemList;
    }

    public String getDurationTime() {
        return durationTime;
    }

    public void setDurationTime(String durationTime) {
        this.durationTime = durationTime;
    }

    public String getEndingDateTime() {
        return endingDateTime;
    }

    public void setEndingDateTime(String endingDateTime) {
        this.endingDateTime = endingDateTime;
    }

    public double getDistance() {
        return distance;
    }

    public void setDistance(double distance) {
        this.distance = distance;
    }
}
