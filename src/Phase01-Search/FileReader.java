import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
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

    public HashMap<String, HashSet<String>> readAllFiles() throws IOException {
        return readFiles(folderPath);
    }

    public HashMap<String, HashSet<String>> readFiles(String path) throws IOException {
        File dataFolder = new File(path);

        HashMap<String, HashSet<String>> result = new HashMap<>();
        for (File file : dataFolder.listFiles()) {
            if (file.isDirectory()) {
                result.putAll(readFiles(file.getPath()));
            } else {
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
        }
        return result;
    }

    public HashSet<String> getFilesNames() throws IOException {
        HashSet<String> filesNames = new HashSet<>();
        Files.walk(Paths.get(folderPath)).filter(Files::isRegularFile).map(Path::getFileName).map(String::valueOf).forEach(filesNames::add);
        return filesNames;
    }
}