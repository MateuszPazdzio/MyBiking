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

            Gson gson = new Gson();
            String jsonInputString = gson.toJson(ride);

            httpURLConnection.setRequestProperty("Content-Type", "application/json");

            try (OutputStream os = httpURLConnection.getOutputStream()) {
                // Replace with your JSON payload
                byte[] input = jsonInputString.getBytes("utf-8");
                os.write(input, 0, input.length);
            }

            int responseCode = httpURLConnection.getResponseCode();
            //TYLKO DLA POST


            InputStream inputStream = httpURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while(line != null){
                line = bufferedReader.readLine();
                data = data + line;
            }


            JSONArray JA = new JSONArray(data);
            for(int i =0 ;i <JA.length(); i++){
                JSONObject JO = (JSONObject) JA.get(i);
                singleParsed =  "Name:" + JO.get("name") + "\n"+
                        "email:" + JO.get("email") + "\n"+
                        "Error:" + JO.get("error") +  "\n";

                dataParsed = dataParsed + singleParsed +"\n" ;


            }

        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }

        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);

    }
}
