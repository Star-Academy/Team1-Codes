package search;

import java.io.IOException;

public class Main {
    static InvertedIndex invertedIndex;
    static FileReader fileReader;

    public static void main(String[] args) throws IOException {
        final String DOCUMENTS_PATH = ".\\src\\search\\documents\\";
        invertedIndex = new InvertedIndex();
        fileReader = new FileReader(DOCUMENTS_PATH);
        fileReader.readAllFiles();
        invertedIndex.initMap();
        new SearchQuery(invertedIndex).processQueries();
    }

    public static FileReader getFileReader() {
        return fileReader;
    }
}
