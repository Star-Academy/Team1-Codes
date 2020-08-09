package search;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.*;
import java.util.stream.Collectors;

import static search.Tokenizer.tokenizeLines;

public class FileReader {
    private final String folderPath;
    private final HashMap<File, List<String>> allFilesTokens;

    public FileReader(String folderPath) {
        this.folderPath = folderPath;
        allFilesTokens = new HashMap<>();
    }

    public void readAllFiles() {
        File dataFolder = new File(folderPath);

        for (File file : Objects.requireNonNull(dataFolder.listFiles()))
            allFilesTokens.put(file, tokenizeLines(getFileLines(file)));
    }

    public List<String> getFileLines(File file) {
        List<String> fileLines = new ArrayList<>();
        try {
            fileLines = Files.readAllLines(file.toPath());
        } catch (IOException e) {
            e.printStackTrace();
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

    public String getFolderPath() {
        return folderPath;
    }


    public HashMap<File, List<String>> getAllFilesTokens() {
        return allFilesTokens;
    }
}
