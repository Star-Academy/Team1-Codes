package search;

import java.io.File;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;

public class InvertedIndex {

    private final HashMap<String, HashSet<String>> allWords = new HashMap<>();

    public void initMap(HashMap<File, List<String>> allTokens) {
        for (File file : allTokens.keySet())
            storeTokens(file, allTokens.get(file));
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

    public HashMap<String, HashSet<String>> getAllWords() {
        return allWords;
    }
}