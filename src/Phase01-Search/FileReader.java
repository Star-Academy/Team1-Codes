import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.util.Arrays;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;


public class FileReader {
    private String folderPath;

    public FileReader(String folderPath) {
        this.folderPath = folderPath;
    }

    public HashMap<String, HashSet<String>> readFiles() throws IOException {
        File dataFolder = new File(folderPath);
        
        HashMap<String, HashSet<String>> result = new HashMap<>();
        for (File file : dataFolder.listFiles()) {
            List<String> fileLines = Files.readAllLines(file.toPath());
            Pattern pattern = Pattern.compile("\\w+");
            for (String line : fileLines) {
                Matcher matcher = pattern.matcher(line);
                while (matcher.find()) {
                    String word = matcher.group().toLowerCase();

                    if (result.containsKey(word)) {
                        HashSet<String> previousOccurrences = result.get(word);
                        previousOccurrences.add(file.getName());
                        result.put(word, previousOccurrences);
                    } else {
                        HashSet<String> newWord = new HashSet<>();
                        newWord.add(file.getName());
                        result.put(word, newWord);
                    }
                }
            }
        }
        return result;
    }
    
    public HashSet<String> getAllFilesNames() {
        File dataFolder = new File(folderPath);
        HashSet<String> filesNames = new HashSet<>();
        Arrays.stream(dataFolder.listFiles()).map(File::getName).forEach(filesNames::add);
        return filesNames;
    }
}