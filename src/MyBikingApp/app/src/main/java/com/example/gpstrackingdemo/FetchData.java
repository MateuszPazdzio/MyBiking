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
    public final String  TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjRlMWVmNDFhLTc4NTgtNGIyYy1iNTdhLTg1M2JkYzM1ZmQ1NyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im1hdGlAd3AucGwiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcm5hbWUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNZW1iZXIiLCJleHAiOjE3MTI3ODc1NTMsImlzcyI6Imh0dHA6Ly9teWJpa2luZy5wbCIsImF1ZCI6Imh0dHA6Ly9teWJpa2luZy5wbCJ9.YSOcp1zkAe8-53hsCONOEbiM7zjqXAmBfNHHZcKEIIc";
    String data ="";
    String dataParsed = "";
    String singleParsed ="";
    private Ride ride;

    public FetchData(Ride ride) {

        this.ride = ride;
    }

    @Override
    protected Void doInBackground(Void... voids) {
        try {
//            URL url = new URL("https://jsonplaceholder.typicode.com/posts");
            URL url = new URL("https://mybiking.azurewebsites.net/api/ride");
            HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();
//            httpURLConnection.setRequestMethod("POST");
//            httpURLConnection.setDoInput(true);
            //TYLKO DLA POST
            httpURLConnection.setRequestMethod("POST");
            httpURLConnection.setDoInput(true);
            httpURLConnection.setDoOutput(true);
            httpURLConnection.setRequestProperty("Authorization","Bearer "+TOKEN);
            Gson gson = new Gson();
            String jsonInputString = gson.toJson(ride);


            httpURLConnection.setRequestProperty("Content-Type", "application/json");

            try (OutputStream os = httpURLConnection.getOutputStream()) {
                // Replace with your JSON payload
                byte[] input = jsonInputString.getBytes("utf-8");
                os.write(input, 0, input.length);
            }

            int responseCode = httpURLConnection.getResponseCode();


        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);

    }
}
