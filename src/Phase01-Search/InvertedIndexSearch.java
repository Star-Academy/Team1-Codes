import java.io.IOException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Scanner;

public class InvertedIndexSearch {
    
    static HashMap<String, HashSet<String>> allWords;
    
    public static void run() throws IOException {
        String documentsPath = ".\\src\\Phase01-Search\\documents";
        
        allWords = new FileReader(documentsPath).readFiles();
        // for (String key : allWords.keySet())
        // System.out.println(key + " " + allWords.get(key));
        processQueries();
    }

    private static void processQueries() {
        Scanner scanner = new Scanner(System.in);
        while (scanner.hasNextLine()) {
            HashSet<String> result = new HashSet<String>();
            String[] query = scanner.nextLine().split(" ");
            for (String word : query) {
                if (true)
                    ;

                if (word.startsWith("+"))
                    result.addAll(orQuery(word.substring(1)));
                else if (word.startsWith("-"))
                    result.addAll(excQuery(word.substring(1)));
                else
                    result.addAll(andQuery(word));
            }
        }
        scanner.close();
    }
    
    static HashSet<String> andQuery(String key) {
        return null;
    }

    static HashSet<String> orQuery(String key) {
        return null;
    }

    static HashSet<String> excQuery(String key) {
        return null;
    }
}
