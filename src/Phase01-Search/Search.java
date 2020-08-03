import java.io.IOException;
import java.util.HashSet;

public class Search {
    static InvertedIndex invertedIndex;

    public static void main(String[] args) throws IOException {
        String documentsPath = ".\\src\\Phase01-Search\\documents";
        invertedIndex.initMap();
        invertedIndex = new InvertedIndex(documentsPath);
        new SearchQuery(invertedIndex).processQueries();
    }

    static void printResult(HashSet<String> result) {
        if (!result.isEmpty())
            System.out.println("Entry has been found in: " + result);
        else
            System.out.println("Entry wasn't found.");
    }
}
