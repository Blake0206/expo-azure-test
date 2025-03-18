import { useState } from 'react';
import { StyleSheet, TextInput, Button, Platform } from 'react-native';
import EditScreenInfo from '@/components/EditScreenInfo';
import { Text, View } from '@/components/Themed';

export default function TabOneScreen() {
	const [name, setName] = useState('');
	const [response, setResponse] = useState('');

	const baseUrl = Platform.OS === 'android' ? 'http://10.0.2.2:7071' : 'http://localhost:7071';

	const handleFetch = async () => {
		try {
			const res = await fetch(`${baseUrl}/api/HttpTrigger1?name=${encodeURIComponent(name)}`);
			const text = await res.text();
			setResponse(text);
		} catch (error) {
			setResponse('Error fetching data');
		}
	};

	return (
		<View style={styles.container}>
			<Text style={styles.title}>Tab One</Text>
			<TextInput style={styles.input} placeholder="Enter name" value={name} onChangeText={setName} />
			<Button title="Send Request" onPress={handleFetch} />
			{response ? <Text style={styles.response}>{response}</Text> : null}
			<View style={styles.separator} lightColor="#eee" darkColor="rgba(255,255,255,0.1)" />
			<EditScreenInfo path="app/(tabs)/index.tsx" />
		</View>
	);
}

const styles = StyleSheet.create({
	container: {
		flex: 1,
		alignItems: 'center',
		justifyContent: 'center',
		padding: 20,
	},
	title: {
		fontSize: 20,
		fontWeight: 'bold',
	},
	input: {
		height: 40,
		width: '80%',
		borderColor: 'gray',
		borderWidth: 1,
		marginVertical: 10,
		paddingHorizontal: 8,
	},
	response: {
		marginTop: 10,
		fontSize: 16,
		color: 'blue',
	},
	separator: {
		marginVertical: 30,
		height: 1,
		width: '80%',
	},
});
