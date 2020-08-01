import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Code {
    public static void main(String[] args) throws IOException {
        Scanner scanner = new Scanner(System.in);

        HashMap<String, HashSet<String>> allWords = new HashMap<>();
        File dataFolder = new File("C:\\Users\\ASC\\Desktop\\training");
        for (File file : Objects.requireNonNull(dataFolder.listFiles())) {
            List<String> fileLines = Files.readAllLines(file.toPath());
            Pattern pattern = Pattern.compile("\\w+");
            for (String line : fileLines) {
                Matcher matcher = pattern.matcher(line);
                while (matcher.find()) {
                    String word = matcher.group().toLowerCase();

                    if (allWords.containsKey(word)) {
                        HashSet<String> previousOccurances = allWords.get(word);
                        previousOccurances.add(file.getName());
                        allWords.put(word, previousOccurances);
                    } else {
                        HashSet<String> newWord = new HashSet<>();
                        newWord.add(file.getName());
                        allWords.put(word, newWord);
                    }
                }
            }
        }

        // for (String key : allWords.keySet())
        //     System.out.println(key + " " + allWords.get(key));
        
        while (scanner.hasNext()) {
            String query = scanner.next();
            if (allWords.containsKey(query))
                System.out.println(allWords.get(query));
            else
                System.out.println("NOT FOUND");
        }

        scanner.close();
    }
}
