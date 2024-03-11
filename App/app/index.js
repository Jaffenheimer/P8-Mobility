import { View, Text, StyleSheet } from "react-native";
import { StatusBar } from 'expo-status-bar';

function Homepage() {
    return (
        <View style={styles.container}>
        <Text>Test App, Does it work?!</Text>
        <StatusBar style="auto" />
      </View>
    );
}

const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: '#fff',
      alignItems: 'center',
      justifyContent: 'center',
    },
});

export default Homepage;
