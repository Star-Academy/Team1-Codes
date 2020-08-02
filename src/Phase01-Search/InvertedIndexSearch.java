import java.io.IOException;
import java.util.ArrayList;
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
            ArrayList<String> orQueries = new ArrayList<>();
            ArrayList<String> excQueries = new ArrayList<>();
            Arrays.stream(query).filter((String k) -> k.startsWith("+")).forEach(orQueries::add);
            Arrays.stream(query).filter((String k) -> k.startsWith("-")).forEach(excQueries::add);

            HashSet<String> result = fileReader.getAllFilesNames(); // is initialized with all files
            for (String word : query)
                if (!word.startsWith("+") && !word.startsWith("-") && allWords.containsKey(word))
                    result.retainAll(allWords.get(word)); // calculates the intersection of results
            for (String word : orQueries)
                if (allWords.containsKey(word.substring(1)))
                    result.addAll(allWords.get(word.substring(1)));
            for (String word : excQueries)
                if (allWords.containsKey(word.substring(1)))
                    result.removeAll(allWords.get(word.substring(1)));

            if (result.isEmpty())
                System.out.println("Entry wasn't found.");
            else
                System.out.println("Entry has been found in: " + result);

        }
        scanner.close();
    }
}
