package search;

import java.io.IOException;
import java.util.HashSet;
import java.util.Scanner;

public class SearchQuery {

    InvertedIndex invertedIndex;
    Query query;

    SearchQuery(InvertedIndex invertedIndex) {
        this.invertedIndex = invertedIndex;
    }

    void processQueries() throws IOException {
        Scanner scanner = new Scanner(System.in);
        while (scanner.hasNextLine()) {
            String[] words = scanner.nextLine().split(" ");
            query = new Query(invertedIndex);
            query.analyzeQuery(words);
            printResult(query.buildResult());
        }
        scanner.close();
    }

    void printResult(HashSet<String> result) {
        if (!result.isEmpty())
            System.out.println("Entry has been found in: " + result);
        else
            System.out.println("Entry wasn't found.");
    }

}