import React from 'react';
import style from './hookForm.scss';
import { Controller, Control } from 'react-hook-form';
import { Select } from 'antd';
import { Honorific } from '../../../api/generated';
import HookSelectContainer from './HookSelectContainer';

const { Option } = Select;

interface Props {
  control: Control;
  onChange?: (e: any[]) => void;
}

function HookHonorificSelect({ control, onChange }: Props) {
  const titleStrings = Object.values(Honorific).filter((v) => typeof v === 'string');

  return (
    <HookSelectContainer>
      <label className={style.hookSelectLabel} htmlFor="honorific">
        Title:
      </label>
      <Controller
        as={
          <Select id="honorific" className={style.honorificSelect}>
            {titleStrings.map((t, i) => (
              <Option key={t} value={i}>
                {t === Honorific[Honorific.NoTitle] ? 'No Title' : t}
              </Option>
            ))}
          </Select>
        }
        onChange={onChange}
        control={control}
        name="honorific"
        defaultValue={Honorific.Mr}
      />
    </HookSelectContainer>
  );
}

export default HookHonorificSelect;
