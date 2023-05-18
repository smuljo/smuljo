import React, { useState, ChangeEvent, FormEvent } from 'react';
import axios from 'axios';
import {Grid, Modal, TextField} from '@mui/material';
import { styled } from '@mui/system';
import MyButton from "./MyButton";

const ModalContainer = styled('div')`
  display: flex;
  justify-content: center;
  margin-top: 10%;
`;

const ModalContent = styled('div')`
  display: flex;
  flex-direction: column;
  justify-content: center;
  background-color: #fff;
  padding: 20px;
`;

const InputField = styled(TextField)`
  width: 100%;
  margin-bottom: 10px;
  margin-top: 10px;

  & .MuiInputBase-root {
    width: 100%;
  }
`;

interface AddSubjectModelProps {
    mainTopicId: number;
}

const AddSubjectModel: React.FC<AddSubjectModelProps> = ({ mainTopicId }) => {
    const [inputValue, setInputValue] = useState('');
    const [isModalOpen, setIsModalOpen] = useState(false);
    const token = localStorage.getItem('accessToken');

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setInputValue(e.target.value);
    };

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const data = {
            title: inputValue,
            mainTopicId: mainTopicId
        };

        const jsonData = JSON.stringify(data);

        axios.post('https://6e70-178-204-52-103.ngrok-free.app/api/topics', jsonData, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })
            .then((response) => {
                console.log(response.data);
                console.log(jsonData);
                setIsModalOpen(false);
            })
            .catch((error) => {
                console.error(error);
            });
    };

    const openModal = () => {
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setIsModalOpen(false);
    };

    return (
        <div>
            <MyButton variant="contained" onClick={openModal}>Добавить предмет</MyButton>

            <Modal
                open={isModalOpen}
                onClose={closeModal}
                aria-labelledby="modal-title"
            >
                <ModalContainer>
                    <ModalContent>
                        <h2 id="modal-title">Добавить предмет</h2>
                        <form onSubmit={handleSubmit}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <InputField
                                        type="text"
                                        name="title"
                                        value={inputValue}
                                        onChange={handleInputChange}
                                        label="Введите название предмета"
                                        variant="outlined"
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <MyButton type="submit" variant="contained">Добавить</MyButton>
                                </Grid>
                                <Grid item xs={3}>
                                    <MyButton variant="contained" onClick={closeModal}>Закрыть</MyButton>
                                </Grid>
                            </Grid>
                        </form>
                    </ModalContent>
                </ModalContainer>
            </Modal>
        </div>
    );
};

export default AddSubjectModel;
