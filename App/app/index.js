import { Stack, Link } from 'expo-router';
import { YStack, ButtonText, View } from 'tamagui';

import { Container, Main, Title, Subtitle, TopHalf, ButtomHalf, Button } from '../tamagui.config';

export default function Page() {
  return (
    <View>
      <Stack.Screen />
      <YStack>
        <Title>Test App</Title>
        <Subtitle>Welcome to this test app</Subtitle>
      </YStack>
      <YStack>
        <Link href={'/location'} asChild>
          <Button style={'padding: 3px'}>
            <ButtonText>Get Location</ButtonText>
          </Button>
        </Link>
      </YStack>
      <YStack>
        <Link href={'/layouttest'} asChild>
          <Button style={'padding: 3px'}>
            <ButtonText>Go To Layout</ButtonText>
          </Button>
        </Link>
      </YStack>
    </View>
  );
}
