import java.io.IOException;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Scanner;

public class InvertedIndexSearch {

    static HashMap<String, HashSet<String>> allWords;
    static FileReader fileReader;

    public static void run() throws IOException {
        String documentsPath = ".\\src\\Phase01-Search\\documents";
        fileReader = new FileReader(documentsPath);
        allWords = fileReader.readFiles();
        processQueries();
    }

    private static void processQueries() {
        Scanner scanner = new Scanner(System.in);
        while (scanner.hasNextLine()) {
            String[] query = scanner.nextLine().split(" ");
            // String[] orQueries = (String[]) Arrays.stream(query).filter((String k) -> k.startsWith("+")).toArray();
            // String[] excQueries = (String[]) Arrays.stream(query).filter((String k) -> k.startsWith("-")).toArray();

            HashSet<String> result = fileReader.getAllFilesNames(); // should be initialized with all files
            System.out.println(result);
            System.exit(0);

            for (String word : query) {
                if (!word.startsWith("+") && !word.startsWith("-"))
                    if (allWords.containsKey(word))
                        result.retainAll(allWords.get(word)); // calculates the intersection of results

                //     result.addAll(orQuery(word.substring(1)));
                // else if (word.startsWith("-"))
                //     result.addAll(excQuery(word.substring(1)));
                // else
                //     result.addAll(andQuery(word));
            }
            System.out.println(result);
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
