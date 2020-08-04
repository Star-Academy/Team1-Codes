package search;

import java.util.ArrayList;
import java.util.HashSet;

public class Query {

    InvertedIndex invertedIndex;

    boolean seenAnOrDoc, seenAnAndDoc;

    public ArrayList<String> orQueries;
    public ArrayList<String> andQueries;
    public ArrayList<String> excQueries;

    public HashSet<String> orDocs;
    public HashSet<String> andDocs;
    public HashSet<String> excDocs;
    public HashSet<String> result;

    public Query(InvertedIndex invertedIndex) {
        seenAnOrDoc = seenAnAndDoc = false;
        orQueries = new ArrayList<>();
        andQueries = new ArrayList<>();
        excQueries = new ArrayList<>();
        orDocs = new HashSet<>();
        andDocs = new HashSet<>();
        excDocs = new HashSet<>();
        result = new HashSet<>();
        this.invertedIndex = invertedIndex;
    }

    public void analyzeQuery(String[] words) {
        for (String word : words)
            switch (word.charAt(0)) {
                case '+':
                    seenAnOrDoc = true;
                    orQueries.add(word.substring(1));
                    break;
                case '-':
                    excQueries.add(word.substring(1));
                    break;
                default:
                    seenAnAndDoc = true;
                    andQueries.add(word);
            }
    }

    public void buildFoundDocs() {
        orDocs = invertedIndex.orQueryResult(orQueries);
        andDocs = invertedIndex.andQueryResult(andQueries);
        excDocs = invertedIndex.excQueryResult(excQueries);
    }

    public HashSet<String> buildResult() {
        // searching the queries
        buildFoundDocs();

        // TODO null object:
        if (andDocs == null) // Uncompatible "And Queries" were found, so the result is empty
            return new HashSet<>();
        if (!seenAnAndDoc && !seenAnAndDoc) // We have only "Exclude Queries", so result is all documents but their results
            result = Main.fileReader.getFilesNames();
        else if (!seenAnOrDoc) // No "Or Queries" were found, so the result is equal to "And Queries" results
            result = andDocs;
        else if (!seenAnAndDoc) // No "And Queries" were found, so the result is equal to "Or Queries" results
            result = orDocs;
        else { // Here we have to calculate the intersection of "And Queries" and "Or Queries"
            andDocs.retainAll(orDocs);
            result = andDocs;
        }
        // In the end, we need to omit all the "Excluded Queries" results and return it..
        result.removeAll(excDocs);

        return result;
    }    
}