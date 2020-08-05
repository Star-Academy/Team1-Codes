package search.test;

import org.junit.*;

import java.io.File;
import java.io.IOException;
import java.nio.file.NoSuchFileException;
import java.util.*;

import search.FileReader;

public class FileReaderTest {
    FileReader fileReader = new FileReader(".\\src\\search\\test\\testDocuments\\");

    @Test
    public void filesNamesTest() {
        HashSet<String> filesNames = fileReader.getFilesNames();
        HashSet<String> expectedFiles = new HashSet<>();
        expectedFiles.add("Team.txt");
        expectedFiles.add("One.txt");
        Assert.assertEquals(expectedFiles, filesNames);
    }

    @Test
    public void tokenizeLinesTest() {
        List<String> fileLines = new ArrayList<>();
        fileLines.add("Star$Academy");
        fileLines.add("phase  three");
        List<String> tokenizedLines = fileReader.tokenizeLines(fileLines);
        String[] tokens = { "star", "academy", "phase", "three" };
        List<String> expectedTokens = new ArrayList<>(Arrays.asList(tokens));
        Assert.assertEquals(expectedTokens, tokenizedLines);
    }

    @Test
    public void readAllFilesTest() {
        try {
            fileReader.readAllFiles();
        } catch (IOException ioe) {
            Assert.fail();
        }
        File team = new File(fileReader.folderPath + "\\Team.txt");
        File one = new File(fileReader.folderPath + "\\One.txt");
        String[] teamTokens = { "star", "academy", "phase", "three" };
        String[] oneTokens = { "unit", "test", "feature", "branch" };
        Assert.assertEquals(Arrays.asList(teamTokens), fileReader.allFilesTokens.get(team));
        Assert.assertEquals(Arrays.asList(oneTokens), fileReader.allFilesTokens.get(one));
    }

    @Test
    public void readFilesLinesTest() {
        File team = new File(fileReader.folderPath + "\\Team.txt");
        List<String> fileLines = fileReader.getFileLines(team);
        String[] teamLines = { "star academy", "phase three" };
        Assert.assertEquals(Arrays.asList(teamLines), fileLines);
    }

    @Test
    public void readInvalidFilesTest() {
        File invalidFile = new File("invalid\\path");
        try {
            fileReader.getFileLines(invalidFile);
        } catch (Exception ex) {
            Assert.assertEquals(NoSuchFileException.class, ex.getClass());
        }
    }
}