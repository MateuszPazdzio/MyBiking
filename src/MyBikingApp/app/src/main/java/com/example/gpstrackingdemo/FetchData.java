package com.example.gpstrackingdemo;

import android.os.AsyncTask;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import com.google.gson.Gson;

public class FetchData extends AsyncTask<Void,Void,Void> {
    private final ApiService apiService;
    private Ride ride;

    public FetchData(Ride ride) {
        this.apiService = new ApiService();
        this.ride = ride;
    }

    @Override
    protected Void doInBackground(Void... voids) {

        if(apiService.Login(new LoginData("",""))==Result.BadReuqest()){
            return null;
        }

        String token = apiService.GetToken();
        if(token!=null || !token.isEmpty()){
             if(apiService.AddRide(ride)==Result.BadReuqest()){
                 return null;
             }
             return null;
        }

        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
    }
}
