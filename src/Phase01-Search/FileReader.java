import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class FileReader {
    private String folderPath;

    public FileReader(String folderPath) {
        this.folderPath = folderPath;
    }

    public void readAllFiles() throws IOException {
        File dataFolder = new File(folderPath);
        
        for (File file : dataFolder.listFiles()) {
            ArrayList<String> fileTokens = getTokens(file.getPath());
            Search.invertedIndex.storeTokens(file, fileTokens);
        }
    }

    public ArrayList<String> getTokens(String filePath) throws IOException {
        File dataFolder = new File(filePath);

        ArrayList<String> fileTokens = new ArrayList<>();

        List<String> fileLines = Files.readAllLines(dataFolder.toPath());
        for (String line : fileLines) {
            Matcher matcher = Pattern.compile("\\w+").matcher(line);
            while (matcher.find())
                fileTokens.add(matcher.group().toLowerCase());
        }
        return fileTokens;
    }
}
