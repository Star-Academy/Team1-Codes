import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Code {

    static HashMap<String, HashSet<String>> allWords;
    public static void main(String[] args) throws IOException {
        Scanner scanner = new Scanner(System.in);

        allWords = new HashMap<>();
        new FileReader(".\\documents").readFiles();

        // for (String key : allWords.keySet())
        //     System.out.println(key + " " + allWords.get(key));

        // Implement an inverted-index class which contains all tokens and list of related documents per each token
        
        while (scanner.hasNextLine()) {
            HashSet<String> result = new HashSet();
            String[] query = scanner.nextLine().split(" ");
            for(String word: query){
                if()

                if(word.startsWith("+")) result.addAll(orQuery(word.substring(1)));
                else if(word.startsWith("-")) result.addAll(excQuery(word.substring(1)));
                else result.addAll(andQuery(word));
            }
        }

        scanner.close();
    }
    static HashSet<String> andQuery(String key){

    }
    static HashSet<String> orQuery(String key){
        
    } static HashSet<String> excQuery(String key){
        
    } 
}

class FileReader {
    private String folderPath;

    public FileReader(String folderPath) {
        this.folderPath = folderPath;
    }

    public void readFiles() throws IOException {
        File dataFolder = new File(folderPath);

        for (File file : Objects.requireNonNull(dataFolder.listFiles())) {
            List<String> fileLines = Files.readAllLines(file.toPath());
            Pattern pattern = Pattern.compile("\\w+");
            for (String line : fileLines) {
                Matcher matcher = pattern.matcher(line);
                while (matcher.find()) {
                    String word = matcher.group().toLowerCase();

                    if (Code.allWords.containsKey(word)) {
                        HashSet<String> previousOccurances = Code.allWords.get(word);
                        previousOccurances.add(file.getName());
                        Code.allWords.put(word, previousOccurances);
                    } else {
                        HashSet<String> newWord = new HashSet<>();
                        newWord.add(file.getName());
                        Code.allWords.put(word, newWord);
                    }
                }
            }
        }
    }
}