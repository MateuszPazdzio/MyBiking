package com.example.gpstrackingdemo;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

public class WheeleRide {
    private List<WheeleItem> wheeleItemList = new ArrayList<>();
    private String durationTime;
    private LocalDateTime startingDateTime;
    private LocalDateTime endingDateTime;
    private String distnace;

    public WheeleRide(LocalDateTime startingDateTime) {
        this.startingDateTime = startingDateTime;
    }

    public void AddIWheeleItem(WheeleItem wheeleItem){
        wheeleItemList.add(wheeleItem);
    }

    public List<WheeleItem> getWheeleItemList() {
        return this.wheeleItemList;
    }
    public void setWheeleItemList(List<WheeleItem> wheeleItemList) {
        this.wheeleItemList = wheeleItemList;
    }

    public String getDurationTime() {
        return durationTime;
    }

    public void setDurationTime(String durationTime) {
        this.durationTime = durationTime;
    }

    public LocalDateTime getEndingDateTime() {
        return endingDateTime;
    }

    public void setEndingDateTime(LocalDateTime endingDateTime) {
        this.endingDateTime = endingDateTime;
    }

    public String getDistnace() {
        return distnace;
    }

    public void setDistnace(String distnace) {
        this.distnace = distnace;
    }
}
