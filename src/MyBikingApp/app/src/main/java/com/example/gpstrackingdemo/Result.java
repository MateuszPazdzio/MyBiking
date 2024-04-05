package com.example.gpstrackingdemo;

public class Result {

    public static Response Ok(){
        return new Response("Ok","Completed");
    }

    public static Response BadReuqest(){
        return new Response("Bad Request","Fail");
    }
}
