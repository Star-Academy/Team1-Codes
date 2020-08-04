package search;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

public class FileReader {
    private String folderPath;
    private HashMap<File, List<String>> allFilesTokens;

    public FileReader(String folderPath) {
        this.folderPath = folderPath;
        allFilesTokens = new HashMap<>();
    }

    public void readAllFiles() throws IOException {
        File dataFolder = new File(folderPath);

        for (File file : dataFolder.listFiles())
            allFilesTokens.put(file, tokenizeLines(getFileLines(file)));
    }

    private List<String> tokenizeLines(List<String> fileLines) {
        ArrayList<String> fileTokens = new ArrayList<>();

        for (String line : fileLines) {
            Matcher matcher = Pattern.compile("\\w+").matcher(line);
            while (matcher.find())
                fileTokens.add(matcher.group().toLowerCase());
        }
        return fileTokens;
    }

    private List<String> getFileLines(File file) {
        List<String> fileLines = new ArrayList<>();
        try {
            fileLines = Files.readAllLines(file.toPath());
        } catch (IOException e) {
            System.err.println("An error occurred during reading files.");
            System.exit(0);
        }
        return fileLines;
    }

    public HashSet<String> getFilesNames() {
        HashSet<String> filesNames = new HashSet<>();
        try {
            filesNames = Files.walk(Paths.get(folderPath)).filter(Files::isRegularFile).map(Path::getFileName)
                    .map(String::valueOf).collect(Collectors.toCollection(HashSet::new));
        } catch (IOException ioe) {
            ioe.printStackTrace();
        }
        return filesNames;
    }

    public HashMap<File, List<String>> getAllFilesTokens() {
        return allFilesTokens;
    }
}