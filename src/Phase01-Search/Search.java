import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Scanner;

public class Search {
    static InvertedIndex invertedIndex = new InvertedIndex();

    public static void main(String[] args) throws IOException {
        String documentsPath = ".\\src\\Phase01-Search\\documents";
        invertedIndex.init(documentsPath);
        processQueries();
    }

    static void processQueries() throws IOException {
        Scanner scanner = new Scanner(System.in);
        while (scanner.hasNextLine()) {
            String[] words = scanner.nextLine().split(" ");
            printResult(buildResult(analyzeQuery(words)));
        }
        scanner.close();
    }

    static QueryInfo analyzeQuery(String[] words) {
        boolean seenAnOrDoc = false, seenAnAndDoc = false;
        ArrayList<String> orQueries = new ArrayList<>();
        ArrayList<String> andQueries = new ArrayList<>();
        ArrayList<String> excQueries = new ArrayList<>();

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

        // wrapper class to solve long parameter list =)
        return new QueryInfo(seenAnOrDoc, seenAnAndDoc, invertedIndex.orQueryResult(orQueries),
                invertedIndex.andQueryResult(andQueries), invertedIndex.excQueryResult(excQueries));
    }

    static HashSet<String> buildResult(QueryInfo queryInfo) {
        HashSet<String> result = new HashSet<>();
        //TODO
        //null object ?!
        if(queryInfo.andDocs == null)
            return new HashSet<>();
        if (!queryInfo.seenAnOrDoc)
            result = queryInfo.andDocs;
        else if (!queryInfo.seenAnAndDoc)
            result = queryInfo.orDocs;
        else {
            queryInfo.andDocs.retainAll(queryInfo.orDocs);
            result = queryInfo.andDocs;
        }
        result.removeAll(queryInfo.excDocs);
        return result;
    }

    static void printResult(HashSet<String> result) {
        if (!result.isEmpty())
            System.out.println("Entry has been found in: " + result);
        else
            System.out.println("Entry wasn't found.");
    }

    static class QueryInfo {
        boolean seenAnOrDoc, seenAnAndDoc;
        HashSet<String> orDocs;
        HashSet<String> andDocs;
        HashSet<String> excDocs;

        public QueryInfo(boolean seenAnOrDoc, boolean seenAnAndDoc, HashSet<String> orDocs, HashSet<String> andDocs,
                HashSet<String> excDocs) {
            this.seenAnOrDoc = seenAnOrDoc;
            this.seenAnAndDoc = seenAnAndDoc;
            this.orDocs = orDocs;
            this.andDocs = andDocs;
            this.excDocs = excDocs;
        }
    }

}
