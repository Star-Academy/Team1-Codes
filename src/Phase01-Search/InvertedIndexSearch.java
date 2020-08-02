import java.io.IOException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Scanner;

public class InvertedIndexSearch {

    static HashMap<String, HashSet<String>> allWords;
    static FileReader fileReader;

    public static void run() throws IOException {
        String documentsPath = ".\\src\\Phase01-Search\\documents";
        fileReader = new FileReader(documentsPath);
        allWords = fileReader.readAllFiles();
        processQueries();
    }

    private static void processQueries() throws IOException {
        Scanner scanner = new Scanner(System.in);
        outer: while (scanner.hasNextLine()) {
            String[] query = scanner.nextLine().split(" ");

            HashSet<String> result = new HashSet<>();
            HashSet<String> orDocs = new HashSet<>();
            HashSet<String> excDocs = new HashSet<>();
            HashSet<String> andDocs = new HashSet<>();

            boolean seenAnOrDoc = false, seenAnAndDoc = false;
            for (String word : query) {
                switch (word.charAt(0)) {
                    case '+':
                        seenAnOrDoc = true;
                        if (allWords.containsKey(word.substring(1)))
                            orDocs.addAll(allWords.get(word.substring(1)));
                        break;
                    case '-':
                        if (allWords.containsKey(word.substring(1)))
                            excDocs.addAll(allWords.get(word.substring(1)));
                        break;

                    default:
                        seenAnAndDoc = true;
                        if (allWords.containsKey(word)) {
                            if (andDocs.isEmpty())
                                andDocs.addAll(allWords.get(word));
                            else
                                andDocs.retainAll(allWords.get(word));
                        } else {
                            System.out.println("Entry wasn't found.");
                            continue outer;
                        }
                }
            }
            if (!seenAnOrDoc)
                result = andDocs;
            else if (!seenAnAndDoc)
                result = orDocs;
            else {
                andDocs.retainAll(orDocs);
                result = andDocs;
            }

            result.removeAll(excDocs);

            if (!result.isEmpty())
                System.out.println("Entry has been found in: " + result);
            else
                System.out.println("Entry wasn't found.");

        }
        scanner.close();
    }
}
