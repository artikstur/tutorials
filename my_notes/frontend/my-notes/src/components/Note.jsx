import {
	Card,
	CardBody,
	CardFooter,
	CardHeader,
	Divider,
	Heading,
	Text,
} from "@chakra-ui/react";
import moment from "moment/moment";

export default function Note({ title, desctiption, createdAt }) {
	return (
		<Card variant={"filled"}>
			<CardHeader>
				<Heading size={"md"}>{title}</Heading>
			</CardHeader>
			<Divider borderColor={"gray"} />
			<CardBody>
				<Text>{desctiption}</Text>
			</CardBody>
			<Divider borderColor={"gray"} />
			<CardFooter>{moment(createdAt).format("DD/MM/YYYY h:mm:ss")}</CardFooter>
		</Card>
	);
}
