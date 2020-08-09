package search;

import java.util.*;

public class QueryResult {

    private final HashMap<String, HashSet<String>> allWords;

    public QueryResult(HashMap<String, HashSet<String>> allWords) {
        this.allWords = allWords;
    }

    public FoundDocs orQueryResult(ArrayList<String> orQueries) {
        HashSet<String> result = new HashSet<>();
        for (String word : orQueries)
            if (allWords.containsKey(word))
                result.addAll(allWords.get(word));

        return new FoundDocs(false, result);
    }

    public FoundDocs andQueryResult(ArrayList<String> andQueries) {
        HashSet<String> result = new HashSet<>();
        for (String word : andQueries) {
            if (allWords.containsKey(word)) {
                if (result.isEmpty())
                    result.addAll(allWords.get(word));
                else
                    result.retainAll(allWords.get(word));
            } else
            return new FoundDocs(true, null);
        }
        return new FoundDocs(false, result);
    }

    public FoundDocs excQueryResult(ArrayList<String> excQueries) {
        HashSet<String> result = new HashSet<>();
        for (String word : excQueries)
            if (allWords.containsKey(word))
                result.addAll(allWords.get(word));
        return new FoundDocs(false, result);
    }
}
