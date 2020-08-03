import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

public class InvertedIndex {

    HashMap<String, HashSet<String>> allWords;
    FileReader fileReader;

    public InvertedIndex(String documentsPath) {
        fileReader = new FileReader(documentsPath);
        allWords = new HashMap<>();
    }

    public void initMap() {        
        try {
            fileReader.readAllFiles();
        } catch (IOException e) {
            System.err.println("An error occurred during reading files.");
            System.exit(0);
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
        for (String word : andQueries){
            if (allWords.containsKey(word)) {
                if (result.isEmpty())
                    result.addAll(allWords.get(word));
                else
                    result.retainAll(allWords.get(word));
            }
            else
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

    public void storeTokens(File file, ArrayList<String> fileTokens) {
        for (String token : fileTokens) {
            if (allWords.containsKey(token)) {
                HashSet<String> previousOccurrences = allWords.get(token);
                previousOccurrences.add(file.getName());
                allWords.put(token, previousOccurrences);
            } else {
                HashSet<String> newWord = new HashSet<>();
                newWord.add(file.getName());
                allWords.put(token, newWord);
            }
        }
    }
}