package search;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;

public class InvertedIndex {

    HashMap<String, HashSet<String>> allWords = new HashMap<>();

    public void initMap() {
        HashMap<File, List<String>> filesTokens = Main.getFileReader().getAllFilesTokens();
        for (File file : filesTokens.keySet())
            storeTokens(file, filesTokens.get(file));
    }

    public void storeTokens(File file, List<String> fileTokens) {
        for (String token : fileTokens) {
            if (allWords.containsKey(token)) {
                allWords.get(token).add(file.getName());
            } else {
                HashSet<String> newWord = new HashSet<>();
                newWord.add(file.getName());
                allWords.put(token, newWord);
            }
        }
    }

    public HashSet<String> orQueryResult(ArrayList<String> orQueries) {
        HashSet<String> result = new HashSet<>();
        for (String word : orQueries)
            if (allWords.containsKey(word))
                result.addAll(allWords.get(word));

        return result;
    }

    public HashSet<String> andQueryResult(ArrayList<String> andQueries) {
        HashSet<String> result = new HashSet<>();
        for (String word : andQueries) {
            if (allWords.containsKey(word)) {
                if (result.isEmpty())
                    result.addAll(allWords.get(word));
                else
                    result.retainAll(allWords.get(word));
            } else
                return null;
        }
        return result;
    }

    public HashSet<String> excQueryResult(ArrayList<String> excQueries) {
        HashSet<String> result = new HashSet<>();
        for (String word : excQueries)
            if (allWords.containsKey(word))
                result.addAll(allWords.get(word));
        return result;
    }

}