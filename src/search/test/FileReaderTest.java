package search.test;

import org.junit.*;

import java.io.File;
import java.nio.file.NoSuchFileException;
import java.util.*;

import search.FileReader;

public class FileReaderTest {
    static final String TEST_DOCS_PATH = ".\\src\\search\\test\\testDocuments\\";
    static FileReader fileReader;

    @BeforeClass
    public static void setUp() {
        fileReader = new FileReader(TEST_DOCS_PATH);
    }

    @Test
    public void filesNamesTest() {
        HashSet<String> filesNames = fileReader.getFilesNames();
        HashSet<String> expectedFiles = new HashSet<>();
        expectedFiles.add("Team.txt");
        expectedFiles.add("One.txt");
        Assert.assertEquals(expectedFiles, filesNames);
    }

    @Test
    public void readAllFilesTest() {
        fileReader.readAllFiles();

        File team = new File(fileReader.getFolderPath() + "\\Team.txt");
        File one = new File(fileReader.getFolderPath() + "\\One.txt");
        String[] teamTokens = {"star", "academy", "phase", "three"};
        String[] oneTokens = {"unit", "test", "feature", "branch"};
        Assert.assertEquals(Arrays.asList(teamTokens), fileReader.getAllFilesTokens().get(team));
        Assert.assertEquals(Arrays.asList(oneTokens), fileReader.getAllFilesTokens().get(one));
    }

    @Test
    public void readFilesLinesTest() {
        File team = new File(fileReader.getFolderPath() + "\\Team.txt");
        List<String> fileLines = fileReader.getFileLines(team);
        String[] teamLines = {"star academy", "phase three"};
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