<template>
    <div>
      <editor-content :editor="editor" />
    </div>
  </template>
  
  <script setup>
  import StarterKit from '@tiptap/starter-kit'
  import { Editor, EditorContent } from '@tiptap/vue-3'
  import {ref, watch, onMounted} from 'vue';
  
  // 定義 props
  const props = defineProps({
    modelValue: {
    type: String,
    default: "",
  }
  })

  
const editor = useEditor({
  content: props.modelValue,
  extensions: [StarterKit],
  onUpdate: ({editor}) => {
    let content = editor.getHTML()
    emit('update:modelValue', content)
  }
})
  
  // 定義 emits
  const emits = defineEmits(['update:modelValue'])
  
  
  // 監聽 modelValue 的變化
  watch(() => props.modelValue, (value) => {
    // HTML
    const isSame = editor.value.getHTML() === value
  
    if (!isSame) {
      editor.value.commands.setContent(value, false)
    }
  })
  
  // 創建 editor
  onMounted(() => {
    editor.value = new Editor({
      extensions: [StarterKit],
      content: props.modelValue,
      onUpdate: () => {
        // HTML
        emits('update:modelValue', editor.value.getHTML())
      },
    })
  })
  

  </script>
  