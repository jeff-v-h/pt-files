import React from 'react';
import style from './hookForm.scss';
import { Controller, Control } from 'react-hook-form';
import { Select } from 'antd';
import { Gender } from '../../../api/generated';
import HookSelectContainer from './HookSelectContainer';

const { Option } = Select;

interface Props {
  control: Control;
}

function HookGenderSelect({ control }: Props) {
  const genderStrings = Object.values(Gender).filter((v) => typeof v === 'string');

  return (
    <HookSelectContainer>
      <label className={style.hookInputLabel}>Gender:</label>
      <Controller
        as={
          <Select className={style.genderSelect}>
            {genderStrings.map((g, i) => (
              <Option key={g} value={i}>
                {g === Gender[Gender.Other] ? 'Other/Unspecified' : g}
              </Option>
            ))}
          </Select>
        }
        control={control}
        name="gender"
        defaultValue="male"
      />
    </HookSelectContainer>
  );
}

export default HookGenderSelect;
