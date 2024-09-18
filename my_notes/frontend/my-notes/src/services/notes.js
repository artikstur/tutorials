import axios from "axios";

export const fetchNotes = async (filter) => {
	try {
		var resposne = await axios.get("http://localhost:5003/notes", {
			params: {
				search: filter?.search,
				sortItem: filter?.sortItem,
				sortOrder: filter?.sortOrder,
			},
		});

		return resposne.data.notes;
	} catch (e) {
		console.error(e);
	}
};

export const createNote = async (note) => {
	try {
		var resposne = await axios.post("http://localhost:5003/notes", note);

		return resposne.status;
	} catch (e) {
		console.error(e);
	}
};
